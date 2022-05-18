package dev.fegaeze;

public class LectureHall {
    private Course course;

    public Course getCourse() { return course; }
    public void setCourse(Course course) {
        if(this.course != course) {
            Course oldValue = this.course;
            this.course = course;
            if(oldValue != null) {
                oldValue.setLectureHall(null);
            }
            if(course != null) {
                course.setLectureHall(this);
            }
        }
    }
}
