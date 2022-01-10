﻿using System;

namespace InventorySystem.Core.Interfaces.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IStoreHouseRepository StoreHouseRepository { get; }

        ICategoryRepository CategoryRepository { get; }

        void SavesChanges();
    }
}
