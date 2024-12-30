﻿using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UserController(
        IUserRepository userRepository,
        IMapper mapper,
        IPhotoService photoService
        ) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await userRepository.GetUsersAsync();
            var usersDto = mapper.Map<IEnumerable<MemberDto>>(users);
            return Ok(usersDto);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MemberDto>> GetUser(int id)
        {
            var user = await userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = mapper.Map<MemberDto>(user);

            return Ok(userDto);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            var user = await userRepository.GetUserByUsernameAsync(username);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = mapper.Map<MemberDto>(user);

            return Ok(userDto);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var user = await userRepository.GetUserByUsernameAsync(User.GetUserName());

            if (user == null)
            {
                return BadRequest("could not find user");
            }

            mapper.Map(memberUpdateDto, user);

            if (await userRepository.SaveAllAsync())
            {
                return NoContent();
            }

            return BadRequest("update fail. try again later");
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var user = await userRepository.GetUserByUsernameAsync(User.GetUserName());

            if (user == null)
            {
                return BadRequest("could not find user");
            }

            var result = await photoService.AddPhotoAsync(file);

            if (result.Error != null)
            {
                return BadRequest(result.Error.Message);
            }

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            user.Photos.Add(photo);

            if (!await userRepository.SaveAllAsync())
            {
                return BadRequest("Problem adding photo");
            }

            return CreatedAtAction(nameof(GetUser), new { username = User.GetUserName() }, mapper.Map<PhotoDto>(photo));
        }
    }
}
