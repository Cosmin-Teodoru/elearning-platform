package via.dk.elearn.models;

import lombok.Data;
import lombok.NoArgsConstructor;
import lombok.experimental.SuperBuilder;

import javax.persistence.*;

@Entity
@Data
@NoArgsConstructor
@SuperBuilder
public class Moderator extends User{
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Long id;

    public Moderator(Long id, String username, String email, String name, String password, String image, String role, boolean approved, int security_level, Country country, University university) {
        super(id, username, email, name, password, image, role, approved, security_level, country, university);
    }

    public Moderator(String username, String email, String name, String password, String image, String role, boolean approved, int security_level, Country country, University university) {
        super(username, email, name, password, image, role, approved, security_level, country, university);
    }
}
