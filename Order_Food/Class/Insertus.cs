using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Order_Food.Class
{
    public partial class Register
    {
        public int User_id { get; set; }
        public string User_user { get; set; }
        [DataType(DataType.Password)]
        public string User_pw { get; set; }
        [DataType(DataType.Password)]
        public string User_repw { get; set; }
        public string User_name { get; set; }
        public string User_last_name { get; set; }
        public Nullable<int> C_FK_Address_U { get; set; }
        public Nullable<int> C_FK_Location_U_id { get; set; }
        public string User_phone { get; set; }
    }
    public class UploadFileModel
    {
        public UploadFileModel()
        {
            Files = new List<HttpPostedFileBase>();
        }
        public List<HttpPostedFileBase> Files { get; set; }
        public string FirstName { get; set; }
    }
    public static class ConvertFile
    {
        public static byte[] ImageToByte(HttpPostedFileBase imageFile)
        {
            if (imageFile == null) { return null; }
            return (imageFile.ContentType == "image/jpeg" || imageFile.ContentType == "image/gif" || imageFile.ContentType == "image/png") ? FileToByte(imageFile) : null;
        }

        public static byte[] FileToByte(HttpPostedFileBase file)
        {
            byte[] data = new byte[file.ContentLength];
            file.InputStream.Read(data, 0, file.ContentLength);
            return data;
        }

        public partial class ProductPhoto
        {
            [DisplayFormat(NullDisplayText = "n/a")]
            //[Required(ErrorMessage = "Please select image file")]
            public HttpPostedFileBase File { get; set; }
        }
    } 
}