namespace WeighingsWEB.Controllers.Response.BasicResponses
{
    public class BooleanResponse {
        public bool response { get; set; }
        public BooleanResponse(bool response) {
            this.response=response;
        }
    }
}