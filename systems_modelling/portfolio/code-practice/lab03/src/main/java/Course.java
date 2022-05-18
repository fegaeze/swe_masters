package dev.fegaeze;

import java.util.Collections;
import java.util.LinkedHashSet;
import java.util.Set;

public class Course {
    private String name;
    private LectureHall lectureHall;
    private LinkedHashSet<Student> students = new LinkedHashSet<Student>();


    public String getName() {
        return name;
    }
    public void setName(String name) {
        this.name = name;
    }
    public Course withName(String name) {
        setName(name);
        return this;
    }


    public LectureHall getLectureHall() {
        return lectureHall;
    }
    public void setLectureHall(LectureHall lectureHall) {
        if(this.lectureHall != lectureHall) {
            LectureHall oldValue = this.lectureHall;
            this.lectureHall = lectureHall;

            if(oldValue != null) {
                oldValue.setCourse(null);
            }

            if(lectureHall != null) {
                lectureHall.setCourse(this);
            }
        }
    }
    public Course withLectureHall(LectureHall lectureHall) {
        setLectureHall(lectureHall);
        return this;
    }


    public Set<Student> getStudents() {
        return Collections.unmodifiableSet(students);
    }
    public boolean addStudent(Student student) {
        return students.add(student);
    }
    public boolean removeStudent(Student student) { return students.remove(student); }
}
