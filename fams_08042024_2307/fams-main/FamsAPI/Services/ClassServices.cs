    using AutoMapper;
using DataLayer.Entities;
using DataLayer.Repositories;
using FamsAPI.IServices;
using FamsAPI.ViewModel;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;

namespace FamsAPI.Services
{
    public class ClassServices : IClass
    {
        private readonly IMapper _mapper;
        private readonly ClassRepository _classRepository;
        private readonly ClassUserRepository _classUserRepository;
        private readonly TrainingProgramRepository _trainingProgramRepository;
        private readonly UserRepository _userRepository;
        private readonly TrainingCalendarRepository _trainingCalendarRepository;
        public ClassServices(ClassRepository classRepository, ClassUserRepository classUserRepository, TrainingProgramRepository trainingProgram, IMapper mapper, UserRepository userRepository, TrainingCalendarRepository trainingCalendarRepository)
        {
            _classRepository = classRepository;
            _classUserRepository = classUserRepository;
            _trainingProgramRepository = trainingProgram;
            _mapper = mapper;
            _userRepository = userRepository;
            _trainingCalendarRepository = trainingCalendarRepository;
        }
        public List<ClassViewModel> GetAllClasses()
        {
            try
            {
                var classes = _classRepository.GetAll().ToList();
                var classViewModels = _mapper.Map<List<ClassViewModel>>(classes);

                foreach (var classViewModel in classViewModels)
                {
                    var trainingProgram = _trainingProgramRepository.Get(tp => tp.TrainingProgramCode == classViewModel.TrainingProgramCode);
                    if (trainingProgram != null)
                    {
                        classViewModel.TrainingProgram = _mapper.Map<TrainingProgramViewModel>(trainingProgram);
                    }
                    var classUsers = _classUserRepository
                                    .GetAll()
                                    .Where(cu => cu.ClassId == classViewModel.ClassID)
                                    .ToList();
                    classViewModel.ClassUsers = _mapper.Map<List<ClassUserViewModel>>(classUsers);

                    var trainingCalendars = _trainingCalendarRepository
                                     .GetAll()
                                     .Where(tc => tc.ClassId == classViewModel.ClassID)
                                     .ToList();
                    classViewModel.TrainingCalendars = _mapper.Map<List<TrainingCalendarViewModel>>(trainingCalendars);
                }

                return classViewModels;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting all classes.", ex);
            }
        }

        public ClassViewModel GetClassById(string classId)
        {
            try
            {
                var classEntity = _classRepository.Get(c => c.ClassID == classId);
                if (classEntity == null)
                {
                    throw new Exception("No class found with this ID");
                }

                var classViewModel = _mapper.Map<ClassViewModel>(classEntity);

                var trainingProgram = _trainingProgramRepository.Get(tp => tp.TrainingProgramCode == classViewModel.TrainingProgramCode);

                classViewModel.TrainingProgram = _mapper.Map<TrainingProgramViewModel>(classEntity.TrainingProgram);

                return classViewModel;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the class by ID.", ex);
            }
        }

        public virtual InputClassViewModel AddNewClass(InputClassViewModel newClass, List<ClassUserViewModel> classUsers, ClaimsPrincipal user)
        {
            try
            {
                var userName = ExtractUserNameFromClaimsPrincipal(user);
                var createClassEntity = _mapper.Map<Class>(newClass);
                createClassEntity.ClassID = GenerateClassId();
                createClassEntity.ClassCode = GenerateClassCode(createClassEntity);
                createClassEntity.CreatedBy = userName;
                createClassEntity.ModifiedBy = userName;

                if (createClassEntity.ClassUsers == null)
                {
                    createClassEntity.ClassUsers = new List<ClassUser>();
                }

                foreach (var classUser in classUsers)
                {
                    var userEntity = _userRepository.Get(u => u.UserId == classUser.UserId); 
                    if (userEntity != null)
                    {
                        var classUserEntity = new ClassUser
                        {
                            Class = createClassEntity,
                            User = userEntity,
                            UserType = classUser.UserType,
                        };
                        createClassEntity.ClassUsers.Add(classUserEntity);
                    }
                }

                _classRepository.Add(createClassEntity);
                _classRepository.SaveChanges();

                return _mapper.Map<InputClassViewModel>(createClassEntity);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating new class.", ex);
            }
        }

        public InputClassViewModel UpdateClass(InputClassViewModel updateClass, ClaimsPrincipal user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user), "ClaimsPrincipal cannot be null.");
                }

                var userName = ExtractUserNameFromClaimsPrincipal(user);

                var existingClass = _classRepository.Get(c => c.ClassID == updateClass.ClassID);
                if (existingClass == null)
                {
                    throw new ArgumentException("Class not found.");
                }

                _mapper.Map(updateClass, existingClass);

                existingClass.ClassCode = GenerateClassCode(existingClass);

                existingClass.ModifiedBy = userName;

                _classRepository.Update(existingClass);
                _classRepository.SaveChanges();

                return _mapper.Map<InputClassViewModel>(existingClass);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the class.", ex);
            }
        }

        private string GenerateClassId()
        {
            int maxSequentialNumber = _classRepository.GetAll()
                .Select(c => int.TryParse(c.ClassID[1..], out int num) ? num : 0)
                .DefaultIfEmpty(0)
                .Max();

            int nextSequentialNumber = maxSequentialNumber + 1;
            if (nextSequentialNumber > 99999999)
            {
                throw new InvalidOperationException("Maximum class count exceeded.");
            }
            string classId = "C" + nextSequentialNumber.ToString("D8");
            return classId;
        }
        private string GenerateClassCode(Class existingClass)
        {
            if (!string.IsNullOrEmpty(existingClass.ClassName))
            {
                char firstLetter = existingClass.ClassName.ToUpper()[0];

                if (firstLetter >= 'A' && firstLetter <= 'Z')
                {
                    int count = _classRepository.GetAll()
                        .Count(c => c.ClassName.ToUpper()[0] == firstLetter && c.ClassID != existingClass.ClassID) + 1;

                    return $"{firstLetter}{count:D3}";
                }
            }
            return null;
        }
        public virtual string ExtractUserNameFromClaimsPrincipal(ClaimsPrincipal user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "ClaimsPrincipal cannot be null.");
            }

            var userNameClaim = user.FindFirst("UserName");

            if (userNameClaim != null)
            {
                return userNameClaim.Value;
            }
            else
            {
                throw new InvalidOperationException("Username claim not found in ClaimsPrincipal.");
            }
        }
    }
}
