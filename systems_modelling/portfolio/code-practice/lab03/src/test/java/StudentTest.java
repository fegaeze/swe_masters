package dev.fegaeze;

import junit.framework.TestCase;
import org.fulib.FulibTools;
import org.junit.Test;

public class StudentTest {
    @Test
    public void testKarliTakesMath() {
        University ut = new University();
        Student karli = new Student().withName("Karli").withUniversity(ut);

        LectureHall audimax = new LectureHall();
        Course math = new Course().withName("Math").withLectureHall(audimax);

        math.addStudent(karli);
        FulibTools.objectDiagrams.dumpSVG("tmp/test1.svg", math);
    }
}