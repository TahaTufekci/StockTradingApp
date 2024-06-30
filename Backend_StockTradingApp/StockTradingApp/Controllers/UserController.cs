using Microsoft.AspNetCore.Mvc;
using StockTradingApp.Interfaces;
using StockTradingApp.Models;
using System;
using System.Collections.Generic;

namespace StockTradingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetUsers();
            return Ok(users);
        }

        [HttpGet("{userId}")]
        public IActionResult GetUser(int userId)
        {
            var user = _userRepository.GetUser(userId);
            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
