using Elearn.Application.ServiceContracts;
using Elearn.GrpcService.Extensions;
using Elearn.Shared.Models;
using ElearnGrpc;
using Grpc.Core;


namespace Elearn.GrpcService.Client;

public class QuestionGrpcClient : IQuestionService
{
    private QuestionService.QuestionServiceClient _questionClient;

    public QuestionGrpcClient()
    {
        var _grpcChannel =
            new Channel("localhost:8843", ChannelCredentials.Insecure);
        _questionClient = new QuestionService.QuestionServiceClient(_grpcChannel);
    }

    public async Task<List<Question>> GetAllQuestionsAsync()
    {
        List<Question> questions = new List<Question>();
        using (var call = _questionClient.GetAllQuestions(new PaginationModel()))
        {
            while (await call.ResponseStream.MoveNext())
            {
                var currentQuestion = call.ResponseStream.Current;
                questions.Add(currentQuestion.AsBase());
            }
        }

        return questions;
    }

    public async Task<Question?> GetQuestionByUrlAsync(string url)
    {
        var questionRequested = new QuestionUrl { Url = url };
        var questionFromDB = await _questionClient.GetQuestionAsync(questionRequested);
        return questionFromDB.AsBase();
    }

    public async Task<Question> CreateNewQuestionAsync(Question question)
    {
        var questionModel = question.AsGrpcModel();
        var createdPostFromGrpc = await _questionClient.CreateNewQuestionAsync(questionModel);
        Question createdLecture = createdPostFromGrpc.AsBase();
        //createdLecture.Author = createdPostFromGrpc.Teacher.AsBase();
        return createdLecture;
    }

    public async Task<List<Question>> GetQuestionByUserIdAsync(long userId)
    {
        List<Question> questions = new List<Question>();
        var request = new QuestionUserId() { UserId = userId };
        using (var call = _questionClient.GetQuestionsByUserId(new QuestionUserId(request)))
        {
            while (await call.ResponseStream.MoveNext())
            {
                var currentQuestion = call.ResponseStream.Current;
                questions.Add(currentQuestion.AsBase());
            }
        }

        return questions;
    }

    public async Task<List<Question>> GetAllQuestionsAsync(int pageNumber, int pageSize)
    {
        List<Question> questions = new List<Question>();
        PaginationModel pagination = new PaginationModel
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
        };
        using (var call = _questionClient.GetAllQuestions(pagination))
        {
            while (await call.ResponseStream.MoveNext())
            {
                var currentQuestion = call.ResponseStream.Current;
                questions.Add(currentQuestion.AsBase());
            }
        }

        return questions;
    }


    public async Task DeleteQuestionAsync(Question questionToDelete)
    {
        var questionModel = questionToDelete.AsGrpcModel();
        await _questionClient.DeleteLectureAsync(questionModel);
   }
   
   
    public async Task<Question> EditQuestionAsync(Question question)
    {
        var questionModel = question.AsGrpcModel();
        var questionFromGrpc = await _questionClient.EditQuestionAsync(questionModel);
        Question editedQuestion = questionFromGrpc.AsBase();
        return editedQuestion;
    }

    public async Task<Question?> GetQuestionByIdAsync(long questionId)
    {
        var questionRequested = new QuestionId { Id = questionId };
        var questionGrpcModel = await _questionClient.GetQuestionByIdAsync(questionRequested);
        return questionGrpcModel.AsBase();
    }
}