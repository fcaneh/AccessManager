using AccessManager.Domain.Enums;

namespace AccessManager.Application.Features.Access.CheckAccess
{
    public record CheckAccessResponse
    {
        public AccessResult Result { get; set; }
    }
}
