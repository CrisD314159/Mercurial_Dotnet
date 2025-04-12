using FluentValidation;
using FluentValidation.Results;
using MercurialBackendDotnet.DB;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;
using MercurialBackendDotnet.Exceptions;
using MercurialBackendDotnet.Model;
using MercurialBackendDotnet.Services.Interfaces;
using MercurialBackendDotnet.Validations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MercurialBackendDotnet.Services.Implementations;

public class SubjectService (MercurialDBContext dBContext, IValidator<CreateSubjectDTO> validatorCreate,
IValidator<UpdateSubjectDTO> validatorUpdate, UserManager<User> userManager
) : ISubjectService
{

  private readonly MercurialDBContext _dbContext = dBContext;
  private readonly  IValidator<CreateSubjectDTO> _validatorCreate = validatorCreate;
  private readonly IValidator<UpdateSubjectDTO> _validatorUpdate = validatorUpdate;
  private readonly UserManager<User> _userManager = userManager;

  /// <summary>
  /// Creates a subject
  /// </summary>
  /// <param name="userId"></param>
  /// <param name="createSubjectDTO"></param>
  /// <returns></returns>
  /// <exception cref="VerificationException"></exception>
  /// <exception cref="EntityNotFoundException"></exception>
  public async Task CreateSubjectDTO(string userId, CreateSubjectDTO createSubjectDTO)
  {
    _validatorCreate.ValidateAndThrow(createSubjectDTO);
    if(await VerifyValidSubject(userId, createSubjectDTO.Title))
    {
      Subject subject = new()
      {
        Name = createSubjectDTO.Title,
        User = await _dbContext.Users.FindAsync(userId) ?? throw new EntityNotFoundException("User not found"),
        LastUpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow)
      };

      await _dbContext.Subjects.AddAsync(subject);
      await _dbContext.SaveChangesAsync();
    }
    
  }
  /// <summary>
  /// Verifies if a subject already exists in the user account
  /// Besides, this method also verifies if user already have reached his maximum
  /// ammount of subjects (15)
  /// </summary>
  /// <param name="userId"></param>
  /// <param name="title"></param>
  /// <returns></returns>
  /// <exception cref="ExceededLimitException"></exception>
  /// <exception cref="VerificationException"></exception>
  private async Task<bool> VerifyValidSubject(string userId, string title)
  {
    if(_dbContext.Subjects.Where(s => s.UserId == userId).Count() > 15) 
    throw new ExceededLimitException("You've reached your maximum ammount of subjects");
    if (await _dbContext.Subjects.AnyAsync(s => s.Name == title && s.UserId == userId))
    throw new VerificationException($"There's already a subject with name {title}");
    return true;
  }

  /// <summary>
  /// Deletes a subject from user accound and database
  /// </summary>
  /// <param name="subjectId"></param>
  /// <returns></returns>
  /// <exception cref="EntityNotFoundException"></exception>
  public async Task DeleteSubject(long subjectId)
  {
    var subject = await _dbContext.Subjects.FindAsync(subjectId) 
    ?? throw new EntityNotFoundException("Subject not found");

    _dbContext.Subjects.Remove(subject);
    await _dbContext.SaveChangesAsync();
  }

  /// <summary>
  /// Get all the user subjects limited by pagination control
  /// </summary>
  /// <param name="userId"></param>
  /// <param name="offset"></param>
  /// <param name="limit"></param>
  /// <returns></returns>
  /// <exception cref="EntityNotFoundException"></exception>
  public async Task<GetUserSubjectsDTO> GetUserSubjects(string userId, int offset, int limit)
  {
   var user = await _userManager.FindByIdAsync(userId)
    ?? throw new EntityNotFoundException("User not found");

    if(!await _userManager.IsEmailConfirmedAsync(user)) throw new UnauthorizedException("You're not verified");

    var subjectsList = await _dbContext.Subjects.Where(s => s.UserId == userId).Select(s => new SubjectDTO(
      s.Id,
      s.Name,
      s.LastUpdatedAt
    )).Skip(offset).Take(limit).ToListAsync();

    return new GetUserSubjectsDTO(subjectsList);

  }

  /// <summary>
  /// Updates the subject (only it's title)
  /// </summary>
  /// <param name="updateSubjectDTO"></param>
  /// <returns></returns>
  /// <exception cref="VerificationException"></exception>
  /// <exception cref="EntityNotFoundException"></exception>
  public async Task UpdateSubject(UpdateSubjectDTO updateSubjectDTO)
  {
    _validatorUpdate.ValidateAndThrow(updateSubjectDTO);

    var subject = await _dbContext.Subjects.FindAsync(updateSubjectDTO.SubjectId) 
    ?? throw new EntityNotFoundException("Subject not found");

    subject.Name = updateSubjectDTO.Title;
    subject.LastUpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);
    await _dbContext.SaveChangesAsync();
  }
}