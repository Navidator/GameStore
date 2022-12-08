﻿using GameStore.DataBase.Repository;
using GameStore.Services.Service_Interfaces;
using System;
using System.Threading.Tasks;

namespace GameStore.DataBase.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly GameStoreContext _context;
        public IGameRepository GameRepository { get; set; }
        public IAuthService AuthenticationService { get; set; }

        public UnitOfWork(GameStoreContext context, IGameRepository gameRepository, IAuthService authService)
        {
            _context = context;
            GameRepository = gameRepository;
            AuthenticationService = authService;
        }
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}