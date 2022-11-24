package via.dk.elearn.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;
import via.dk.elearn.models.User;

import java.util.List;

@Repository
public interface UserRepository  extends JpaRepository<User,Long> {
    List<User> findByUsername(String username);
    List<User> findByUsernameContaining(String username);
}
