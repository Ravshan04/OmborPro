using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmborPro.Application.Common.Interfaces;
using OmborPro.Application.DTOs.Auth;
using OmborPro.Domain.Entities;
using System.Security.Claims;
using AutoMapper;

namespace OmborPro.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
    private readonly IRepository<User> _userRepository;
    private readonly IMapper _mapper;

    public ProfilesController(IRepository<User> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdString)) return Unauthorized();

        var userId = Guid.Parse(userIdString);
        var user = await _userRepository.GetByIdAsync(userId);
        
        if (user == null) return NotFound();

        return Ok(_mapper.Map<ProfileDto>(user));
    }

    [HttpPut("me")]
    public async Task<IActionResult> UpdateMe([FromBody] UpdateProfileRequest request)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdString)) return Unauthorized();

        var userId = Guid.Parse(userIdString);
        var user = await _userRepository.GetByIdAsync(userId);
        
        if (user == null) return NotFound();

        _mapper.Map(request, user);
        await _userRepository.UpdateAsync(user);

        return Ok(_mapper.Map<ProfileDto>(user));
    }
}
