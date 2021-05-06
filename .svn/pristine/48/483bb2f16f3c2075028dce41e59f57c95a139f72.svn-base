using SYS_SGI.Domain.Implementation.Common.Base;

namespace SYS_SGI.Domain.Implementation.Entities.BASE
{
    public class AppResponse
    {
        private string _code = Enums.Response.Exception;

        public string Code
        {
            get
            {
                return this._code;
            }
            set
            {
                _code = string.IsNullOrEmpty(value) ? Enums.Response.Exception : value;
            }
        }

        public string Description { get; set; }

        public void SetError(string MessageError)
        {
            this.Code = Enums.Response.Error;
            this.Description = NormalizeDescription(MessageError);
        }
        public void SetException(string MessageException)
        {
            this.Code = Enums.Response.Exception;
            this.Description = NormalizeDescription(MessageException);
        }
        public void SetSuccess(string MessageSuccess)
        {
            this.Code = Enums.Response.Success;
            this.Description = NormalizeDescription(MessageSuccess);
        }
        private string NormalizeDescription(string description)
        {
            if (!string.IsNullOrEmpty(description))
                description = description.Length >= 203 ? description.Substring(0, 200) + "..." : description;
            return description;
        }
    }
}