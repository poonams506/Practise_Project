using Microsoft.AspNetCore.Mvc;
using MembershipApi.Repository.Interface;
using MebershipApI.Moduels.Domain;
using MebershipApI.Moduels.Dto;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MebershipApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMembers _membersRepository;

        public MemberController(IMembers membersRepository)
        {
            _membersRepository = membersRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMembers()
        {
            var members = await _membersRepository.GetAllMembersAsync();
            var memberDtos = members.Select(m => new MemberDto
            {
                Id = m.Id,
                Name = m.Name,
                SocietyName = m.SocietyName,
                GenderName = m.GenderName,
                MemberCategoryName = m.MemberCategoryName,
                HobbyList = m.HobbyList,  // Include HobbyList

                IsActive = m.IsActive
            }).ToList();

            return Ok(memberDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMemberById(Guid id)
        {
            var member = await _membersRepository.GetMemberByIdAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            var memberDto = new MemberDto
            {
                Id = member.Id,
                Name = member.Name,
                SocietyName = member.SocietyName,
                GenderName = member.GenderName,
                MemberCategoryName = member.MemberCategoryName,
                HobbyList = member.HobbyList,  // Include HobbyList

                IsActive = member.IsActive
            };

            return Ok(memberDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMember([FromBody] CreateMemberDto createMemberDto)
        {
            if (createMemberDto == null)
            {
                return BadRequest("Member data is null");
            }

            var createdMember = await _membersRepository.CreateMemberAsync(createMemberDto);

            var memberDto = new MemberDto
            {
                Id = createdMember.Id,
                Name = createdMember.Name,
                SocietyName = createdMember.SocietyName,
                GenderName = createdMember.GenderName,
                MemberCategoryName = createdMember.MemberCategoryName,
                HobbyList = createdMember.HobbyList,  // Include HobbyList

                IsActive = createdMember.IsActive
            };

            return CreatedAtAction(nameof(GetMemberById), new { id = createdMember.Id }, memberDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(Guid id, [FromBody] UpdateMemberDto updateMemberDto)
        {
            if (id != updateMemberDto.Id)
            {
                return BadRequest("Member ID mismatch");
            }

            var updatedMember = await _membersRepository.UpdateMemberAsync(updateMemberDto);
            if (updatedMember == null)
            {
                return NotFound();
            }

            var memberDto = new MemberDto
            {
                Id = updatedMember.Id,
                Name = updatedMember.Name,
                SocietyName = updatedMember.SocietyName,
                GenderName = updatedMember.GenderName,
                MemberCategoryName = updatedMember.MemberCategoryName,
                HobbyList = updatedMember.HobbyList,

                IsActive = updatedMember.IsActive
            };

            return Ok(memberDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(Guid id)
        {
            var member = await _membersRepository.DeleteMemberAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
