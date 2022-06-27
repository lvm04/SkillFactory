using SF.Module35.Models;
using Microsoft.EntityFrameworkCore;

namespace SF.Module35.Data.Repository
{
    public class FriendsRepository : Repository<Friend>
    {
        public FriendsRepository(ApplicationDbContext db) : base(db)
        {

        }

        public async Task AddFriend(User target, User Friend)
        {
            var friends = Set.FirstOrDefault(x => x.UserId == target.Id && x.CurrentFriendId == Friend.Id);

            if (friends == null)
            {
                var item = new Friend()
                {
                    UserId = target.Id,
                    User = target,
                    CurrentFriend = Friend,
                    CurrentFriendId = Friend.Id,
                };

                await Create(item);
            }
        }

        public async Task<List<User>> GetFriendsByUser(User target)
        {
            var friends_ = await Set.Include(x => x.CurrentFriend).ToListAsync();
            var friends = friends_.Where(x => x.UserId == target.Id).Select(x => x.CurrentFriend);  // здесь была ошибка x.User.Id

            return friends.ToList();
        }


        public async Task DeleteFriend(User target, User Friend)
        {
            var friends = Set.FirstOrDefault(x => x.UserId == target.Id && x.CurrentFriendId == Friend.Id);

            if (friends != null)
            {
                await Delete(friends);
            }
        }

    }
}
