﻿using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Ambev.DeveloperEvaluation.NoSQL.Repositories;

public class CartRepository : ICartRepository
{
    private readonly IMongoCollection<Cart> _collection;

    public CartRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<Cart>("carts");
    }

    public async Task CreateOrUpdateCart(Cart cart, CancellationToken cancellationToken = default)
    {
        var filter =
            Builders<Cart>.Filter.And(
            Builders<Cart>.Filter.Eq(e => e.UserId, cart.UserId),
            Builders<Cart>.Filter.Eq(e => e.BranchId, cart.BranchId));

        var update = Builders<Cart>.Update
                        .Set(s => s.UpdatedAt, cart.UpdatedAt)
                        .Set(s => s.Items, cart.Items)
                        .SetOnInsert(s => s.UserId, cart.UserId)
                        .SetOnInsert(s => s.BranchId, cart.BranchId)
                        .SetOnInsert(s => s.Id, ObjectId.GenerateNewId().ToString());

        await _collection.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true }, cancellationToken);
    }

    public async Task<bool> DeleteCart(CartFilter cartFilter, CancellationToken cancellationToken = default)
    {
        var filter =
            Builders<Cart>.Filter.And(
            Builders<Cart>.Filter.Eq(e => e.UserId, cartFilter.UserId),
            Builders<Cart>.Filter.Eq(e => e.BranchId, cartFilter.BranchId));

        var result = await _collection.DeleteOneAsync(filter, cancellationToken);

        return result.DeletedCount.Equals(1);
    }

    public async Task<Cart?> GetCartByUser(CartFilter cartFilter, CancellationToken cancellationToken = default)
    {
        var filter =
            Builders<Cart>.Filter.And(
            Builders<Cart>.Filter.Eq(e => e.UserId, cartFilter.UserId),
            Builders<Cart>.Filter.Eq(e => e.BranchId, cartFilter.BranchId));

        var cart = await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);

        return cart;
    }
}
;