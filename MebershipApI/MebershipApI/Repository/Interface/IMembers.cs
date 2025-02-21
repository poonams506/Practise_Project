using MebershipApI.Moduels.Domain;
using MebershipApI.Moduels.Dto;

namespace MembershipApi.Repository.Interface
{
    public interface IMembers
    {
        Task<IEnumerable<Members>> GetAllMembersAsync();
        Task<Members?> GetMemberByIdAsync(Guid id);
        Task<Members?> CreateMemberAsync(CreateMemberDto dto);
        Task<Members?> UpdateMemberAsync(UpdateMemberDto dto);  // Updated to receive UpdateMemberDto
        Task<Members?> DeleteMemberAsync(Guid id);
    }
}
