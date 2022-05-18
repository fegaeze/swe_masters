package dev.fegaeze;

public class Student {
    private String name;
    private University university;


    public String getName() {
        return name;
    }
    public void setName(String name) {
        if(name == null) {
            throw new IllegalArgumentException("Student name must not be null");
        }
        this.name = name;
    }
    public Student withName(String name) {
        setName(name);
        return this;
    }


    public University getUniversity() {
        return university;
    }
    public void setUniversity(University university) {
        if(this.university != university) {
            University oldValue = this.university;
            this.university = university;

            if(oldValue != null) {
                oldValue.removeStudent(this);
            }
            if(university != null) {
                university.addStudent(this);
            }
        }
    }
    public Student withUniversity(University university) {
        setUniversity(university);
        return this;
    }
}
