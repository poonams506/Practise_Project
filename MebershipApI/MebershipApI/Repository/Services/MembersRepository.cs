using Microsoft.EntityFrameworkCore;
using MembershipApi.Repository.Interface;
using MebershipApI.Moduels.Dto;
using MebershipApI.Data;
using MebershipApI.Moduels.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MembershipApi.Repository.Services
{
    public class MembersRepository : IMembers
    {
        private readonly ApplicationDbContext _context;

        public MembersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Members>> GetAllMembersAsync()
        {
            return await _context.MemberList.ToListAsync();
        }

        public async Task<Members?> GetMemberByIdAsync(Guid id)
        {
            return await _context.MemberList.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Members?> CreateMemberAsync(CreateMemberDto dto)
        {
            var newMember = new Members
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                SocietyName = dto.SocietyName,
                GenderName = dto.GenderName,
                MemberCategoryName = dto.MemberCategoryName,
                HobbyList = dto.HobbyList, // Handle HobbyList

                IsActive = dto.IsActive
            };

            await _context.MemberList.AddAsync(newMember);
            await _context.SaveChangesAsync();
            return newMember;
        }

        public async Task<Members?> UpdateMemberAsync(UpdateMemberDto dto)
        {
            var existingMember = await _context.MemberList.FirstOrDefaultAsync(m => m.Id == dto.Id);
            if (existingMember == null)
            {
                return null;
            }

            existingMember.Name = dto.Name;
            existingMember.SocietyName = dto.SocietyName;
            existingMember.GenderName = dto.GenderName;
            existingMember.MemberCategoryName = dto.MemberCategoryName;
            existingMember.HobbyList = dto.HobbyList;  // Update HobbyList
            existingMember.IsActive = dto.IsActive;

            await _context.SaveChangesAsync();
            return existingMember;
        }

        public async Task<Members?> DeleteMemberAsync(Guid id)
        {
            var member = await _context.MemberList.FirstOrDefaultAsync(m => m.Id == id);
            if (member == null)
            {
                return null;
            }

            _context.MemberList.Remove(member);
            await _context.SaveChangesAsync();
            return member;
        }
    }
}
