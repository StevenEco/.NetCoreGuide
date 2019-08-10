using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreStudyList和Dict
{
    class Student
    {
        public Student(int id,string name)
        {
            StudentId = id;
            Name = name;
        }
        public int StudentId { get; set; }
        public string Name { get; set; }
    }
    class Course
    {
        public int CourseId { get; set; }
        public string CName { get; set; }
        public Course(int sid,string name)
        {
            CourseId = sid;
            CName = name;
        }
    }
    class SC
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public SC(int cid,int sid)
        {
            CourseId = cid;
            StudentId = sid;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>()
            {
                new Student(1,"Mike"),
                new Student(2,"Jack"),
                new Student(3,"David")
            };
            List<Course> courses = new List<Course>()
            {
                new Course(1,"CSE"),
                new Course(2,"CN"),
                new Course(3,"SWE")
            };
            List<SC> sCs = new List<SC>()
            {
                new SC(1,1),
                new SC(1,2),
                new SC(1,3),
                new SC(2,3),
                new SC(3,2)
            };
            //筛选名称
            var temp = from stu in students
                       where stu.Name == "Jack"
                       select stu;
            //级联多重查询，查询所有学生选课信息
            var temp1 = from stu in students
                        join scs in sCs on stu.StudentId equals scs.StudentId
                        join c in courses  on scs.CourseId equals c.CourseId
                        select new {stu.Name,c.CName};
            foreach(var t in temp1)
            {
                Console.WriteLine("Name:{0},Course:{1}",t.Name,t.CName);
            }
        }
    }
}
