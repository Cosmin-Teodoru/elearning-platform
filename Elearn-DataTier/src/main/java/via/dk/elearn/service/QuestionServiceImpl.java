package via.dk.elearn.service;

import com.google.protobuf.Any;
import com.google.rpc.ErrorInfo;
import io.grpc.protobuf.StatusProto;
import io.grpc.stub.StreamObserver;
import org.lognet.springboot.grpc.GRpcService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.data.domain.Sort;
import via.dk.elearn.models.Question;
import via.dk.elearn.protobuf.*;
import via.dk.elearn.repository.QuestionRepository;
import via.dk.elearn.service.mapper.QuestionMapper;

import java.util.List;

@GRpcService
public class QuestionServiceImpl extends QuestionServiceGrpc.QuestionServiceImplBase {
    private QuestionRepository questionRepository;

    @Autowired
    public QuestionServiceImpl(QuestionRepository questionRepository) {
        this.questionRepository = questionRepository;
    }

    @Override
    public void getQuestion(QuestionUrl request, StreamObserver<QuestionModel> responseObserver) {
        List<Question> questions = questionRepository.findByUrl(request.getUrl());
        if (questions.isEmpty()) {
            com.google.rpc.Status status = com.google.rpc.Status.newBuilder()
                    .setCode(com.google.rpc.Code.NOT_FOUND.getNumber())
                    .setMessage("The question is not found")
                    .addDetails(Any.pack(ErrorInfo.newBuilder()
                            .setReason("question not found")
                            .build()))
                    .build();
            responseObserver.onError(StatusProto.toStatusRuntimeException(status));
            return;
        }
        Question question = questions.get(0);
        QuestionModel questionModel = QuestionMapper.convertQuestionToGrpcModel(question);
        responseObserver.onNext(questionModel);
        responseObserver.onCompleted();
    }

    @Override
    public void getAllQuestions(PaginationModel request, StreamObserver<QuestionModel> responseObserver) {
        Pageable sortedByDate =
                PageRequest.of(request.getPageNumber(), request.getPageSize());
        Page<Question> questions = questionRepository.findAll(sortedByDate);
        for (Question question : questions) {
            QuestionModel questionModel = QuestionMapper.convertQuestionToGrpcModel(question);
            responseObserver.onNext(questionModel);
        }
        responseObserver.onCompleted();
    }
    
    @Override
    public void createNewQuestion(QuestionModel request, StreamObserver<QuestionModel> responseObserver) {
        Question question = QuestionMapper.convertGrpcModelToQuestion(request);
        Question questionFromDB = questionRepository.save(question);
        QuestionModel questionGrpcModel = QuestionMapper.convertQuestionToGrpcModel(questionFromDB);
        responseObserver.onNext(questionGrpcModel);
        responseObserver.onCompleted();
    }


    @Override
    public void getQuestionsByUserId(QuestionUserId request, StreamObserver<QuestionModel> responseObserver) {
        List<Question> questions = questionRepository.getQuestionByStudentId(request.getUserId());
        if (questions.isEmpty()) {
            com.google.rpc.Status status = com.google.rpc.Status.newBuilder()
                    .setCode(com.google.rpc.Code.NOT_FOUND.getNumber())
                    .setMessage("The questions are not found")
                    .addDetails(Any.pack(ErrorInfo.newBuilder()
                            .setReason("Questions not found")
                            .build()))
                    .build();
            responseObserver.onError(StatusProto.toStatusRuntimeException(status));
            responseObserver.onCompleted();
        } else {
            for (Question question : questions) {
                QuestionModel questionModel = QuestionMapper.convertQuestionToGrpcModel(question);
                responseObserver.onNext(questionModel);
            }
            responseObserver.onCompleted();
        }
    }
}
