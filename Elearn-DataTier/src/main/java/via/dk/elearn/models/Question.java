package via.dk.elearn.models;

import lombok.Data;

import javax.persistence.*;

@Entity
@Data
public class Question {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Long id;

    @Column(name = "title", length = 50)
    private String title;

    @Column(name = "description", length = 80)
    private String description;

    @Lob
    @Column(name = "body")
    private String body;

    @Column(name = "url")
    private String url;

    //TODO: Does question have course?
    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "course_id")
    private Course course;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "user_id")
    private User user;

    public Question(String body, String title, String url) {
        this.body = body;
        this.title = title;
        this.url = url;
    }

    public Question() {

    }
}
