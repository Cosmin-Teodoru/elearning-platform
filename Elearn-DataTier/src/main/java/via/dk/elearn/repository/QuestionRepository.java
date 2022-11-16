package via.dk.elearn.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import via.dk.elearn.models.Question;

@Repository
public interface QuestionRepository extends JpaRepository<Question,Long> {
}
