﻿using Passenger.Core.Repositories;
using System;
using Passenger.Infrastructure.DTO;
using AutoMapper;
using Passenger.Core.Domain;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Passenger.Infrastructure.Services
{

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IEncrypter _encrypter;
        public UserService(IUserRepository userRepository,IEncrypter encrypter, IMapper mapper)
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> BrowseAsync()
        {
            var users = await _userRepository.BrowseAsync();
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetAsync(string email)
        {
            var user = await _userRepository.GetAsync(email);
            return _mapper.Map<User, UserDto>(user);         
        }

        public async Task LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
            {
                throw new Exception($"Invalid credentials");
            }

            
            var hash = _encrypter.GetHash(password, user.Salt);
            if (user.Password == hash)
            {
                return;
            }
            throw new Exception("Invalid credentials");

        }

        public async Task RegisterAsync(Guid userId,string email, string username, string password, string role)
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null)
            {
                throw new Exception($"User with email {email} exists");
            }
            var salt = _encrypter.GetSalt(password);
            var hash = _encrypter.GetHash(password, salt);
            user = new User(userId,email, username, hash,role, salt);
            await _userRepository.AddAsync(user);
        }

        
    }
}