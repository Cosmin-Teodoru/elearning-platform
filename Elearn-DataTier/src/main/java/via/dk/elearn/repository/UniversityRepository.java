package via.dk.elearn.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import via.dk.elearn.entity.University;

@Repository
public interface UniversityRepository  extends JpaRepository<University,Long> {
}
