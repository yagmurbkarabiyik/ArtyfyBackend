﻿namespace ArtyfyBackend.Core.UnitOfWork
{
	public interface IUnitOfWork
	{
		//For async operations
		Task CommitAsync();

		//For sync operations
		void Commit();
	}
}