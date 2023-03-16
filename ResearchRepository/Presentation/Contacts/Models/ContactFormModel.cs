namespace ResearchRepository.Presentation.Contacts.Models
{
    public class ContactFormModel
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Organization { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }

        public ContactFormModel()
        {
            Name = null!;
            Subject = null!;
            Organization = null!;
            Email = null!;
            Message = null!;
        }
    }
}
