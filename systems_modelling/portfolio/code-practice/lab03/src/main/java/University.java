package dev.fegaeze;

import java.util.LinkedHashSet;
import java.util.Set;

public class University {
    private String name;
    private Set<Student> students = new LinkedHashSet<Student>();


    public String getName() {
        return name;
    }
    public void setName(String name) {
        this.name = name;
    }
    public University withName(String name) {
        setName(name);
        return this;
    }

    public boolean addStudent(Student student) {
        boolean changed = students.add(student);
        if(changed && student != null) {
            student.setUniversity(this);
        }
        return changed;
    }
    public boolean removeStudent(Student student) {
        boolean changed = students.remove(student);
        if(changed && student != null) {
            student.setUniversity(null);
        }
        return changed;
    }
}
