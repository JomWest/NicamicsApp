namespace NicamicsApp.Models.UserRequest
{
    public class AgregarCarritoRequest
    {
        public string UserId { get; set; }
        public string ComicId { get; set; }
        public int Cantidad { get; set; }
    }
}
