package via.dk.elearn.models;

import lombok.Data;

import javax.persistence.DiscriminatorValue;
import javax.persistence.Entity;

@Entity
@Data
public class Student extends User{

    public Student() {
    }

    public Student(String username, String email, String name, String password, String role, int security_level) {
        super(username, email, name, password, role, security_level);
    }
}
