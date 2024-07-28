using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;     

namespace MyFriends.Models
{
    public class Friend
    {
        public Friend()
        {
            Images = new List<Image>();
        }
        [Key] public int ID { get; set; }
        [Display(Name ="שם פרטי")] public string FirstName  { get; set; } = string.Empty;
        [Display(Name = "שם משפחה")] public string LastName { get; set; } = string.Empty;
        [Display(Name = "כתובת מייל")][EmailAddress(ErrorMessage = "כתובת מייל אינה נכונה")] public string Email { get; set; }
        [Display(Name = "מספר טלפון")][Phone(ErrorMessage = "מספר טלפון אינו נכון")] public string PhoneNumber { get; set; }
        [Display (Name = "שם מלא"), NotMapped] public string FullName { get { return FirstName + " " + LastName; } }
        public List<Image> Images { get; set; }
        [Display(Name = "הוספת תמונה"),NotMapped] public IFormFile setImage { get { return null; } set { if (value == null) { return; } AddImage(value); } }
        public void AddImage(IFormFile image)
        {
            //יצירת מקום בזיכרון המכיל קובץ
            MemoryStream stream = new MemoryStream();
            image.CopyTo(stream);
            //הפיכת המקום בזכרון לבייטים
            AddImage(stream.ToArray());
        }

        public void AddImage(byte[] image)
        { 
            Images.Add(new Image { myImage = image, friend = this });
        }
    }
}
