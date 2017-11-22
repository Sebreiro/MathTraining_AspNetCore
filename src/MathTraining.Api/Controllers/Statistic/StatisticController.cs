using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathTraining.Api.Params;
using MathTraining.Application;
using MathTraining.Data.Common;
using MathTraining.Data.Domain.Identity;
using MathTraining.Data.Domain.Statistic;
using MathTraining.Service.Common.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MathTraining.Api.Controllers.Statistic
{
    [Route("api/[controller]/[action]")]
    public class StatisticController:ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<StatisticController> _logger;
        private readonly IAccountService<ApplicationUser, ApplicationRole> _accountService;

        public StatisticController(
            IUnitOfWork unitOfWork, 
            ILogger<StatisticController> logger, 
            IAccountService<ApplicationUser, ApplicationRole> accountService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _accountService = accountService;
        }

        [HttpPost]
        public async Task AddExerciseStatistic([FromBody] ExerciseStatisticParam statistic)
        {
            var user = await _accountService.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogError("Attempt to add Exercise statistic without login");
                return;
            }

            if (statistic == null)
                return;

            var responseTime = TimeSpan.FromMilliseconds(statistic.ResponseTime);

            var eStatistic = new ExerciseStatistic()
            {
                Operator = statistic.Operator,
                FirstNumber = statistic.FirstNumber,
                SecondNumber = statistic.SecondNumber,
                UserResult = statistic.UserResult,
                IsResultCorrect = statistic.IsResultCorrect,
                UserResponsetime = responseTime,
                Date = DateTimeOffset.UtcNow,
                User = user
            };

            _unitOfWork.ExerciseStatisticRepository.Add(eStatistic);

            _unitOfWork.Commit();
        }

        [HttpGet]
        public async Task<ResponseTimeResultParam> GetResponseTimetStatistic()
        {
            var user =await _accountService.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogError("Attempt to get Response time statistic without login");
                return new ResultBasicParam(false, "User not found") as ResponseTimeResultParam;
            }

            var data = _unitOfWork.ExerciseStatisticRepository
                .Filtered(x => x.User == user)
                .GroupBy(x => x.Date.ToLocalTime().Date)
                .Select(group => new ResponseTimeDataResultParam
                {
                    Date = group.Key,
                    AverageResponseTime = group.Sum(x => x.UserResponsetime.TotalMilliseconds) / group.Count(),
                    MedianResponseTime=MedianCalculationHelper.CalculateMedian(group.Select(x=>x.UserResponsetime.TotalMilliseconds).ToList())
                })
                .ToList();

            return new ResponseTimeResultParam(success: true, message: "Success", data: data);
        }
        

        [HttpGet]
        public async Task<AnswerPercentageResultParam> GetAnswersPercentage()
        {
            var user = await _accountService.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogError("Attempt to get Answer percentage statistic without login");
                return new ResultBasicParam(false, "User not found") as AnswerPercentageResultParam;
            }

            var data = _unitOfWork.ExerciseStatisticRepository
                .Filtered(x => x.User == user)
                .GroupBy(x => x.Date.ToLocalTime().Date)
                .Select(group => new
                {
                    Date = group.Key,
                    CorrectPercent = (float)group.Count(x => x.IsResultCorrect) / group.Count() * 100,
                })
                .ToList()
                .Select(g => new AnswerPercentageDataResultParam
                {
                    Date=g.Date,
                    CorrectPercent = Math.Round(g.CorrectPercent, 2, MidpointRounding.AwayFromZero),
                    IncorrectPercent = Math.Round(100 - g.CorrectPercent, 2, MidpointRounding.AwayFromZero)
                })
                .ToList();
            return new AnswerPercentageResultParam(success: true, message: "Success", data: data);
        }

    }

    public class ExerciseStatisticParam
    {
        public ExerciseOperatorType Operator;
        public float FirstNumber;
        public float SecondNumber;
        public float? UserResult;
        public bool IsResultCorrect;
        //Time in ms
        public int ResponseTime;
    }

    public class AnswerPercentageResultParam:ResultBasicParam
    {
        public List<AnswerPercentageDataResultParam> Data;

        public AnswerPercentageResultParam(bool success, string message, List<AnswerPercentageDataResultParam>  data) : base(success, message)
        {
            Data = data;
        }
    }

    public class AnswerPercentageDataResultParam
    {
        public DateTime Date;
        public double CorrectPercent;
        public double IncorrectPercent;
    }

    public class ResponseTimeResultParam : ResultBasicParam
    {
        public List<ResponseTimeDataResultParam> Data;
        public ResponseTimeResultParam(bool success, string message, List<ResponseTimeDataResultParam> data) : base(success, message)
        {
            Data = data;
        }
    }

    public class ResponseTimeDataResultParam
    {
        public DateTime Date;
        public double AverageResponseTime;
        public double MedianResponseTime;
    }
}
