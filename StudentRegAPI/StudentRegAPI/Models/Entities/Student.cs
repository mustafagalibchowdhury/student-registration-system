namespace StudentRegAPI.Models.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string StudetCardID { get; set; }
        public string Name { get; set; }
        public string Father { get; set; }
        public string Mother { get; set; }
        public DateTime DOB { get; set; }

    }
}
