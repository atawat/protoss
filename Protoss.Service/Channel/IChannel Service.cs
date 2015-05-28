using System.Linq;
using Protoss.Entity.Model;
using YooPoon.Core.Autofac;

namespace Protoss.Service.Channel 
{
	public interface IChannelService : IDependency
	{
		ChannelEntity Create (ChannelEntity entity);

		bool Delete(ChannelEntity entity);

		ChannelEntity Update (ChannelEntity entity);

		ChannelEntity GetChannelById (int id);

		IQueryable<ChannelEntity> GetChannelsByCondition(ChannelSearchCondition condition);

		int GetChannelCount (ChannelSearchCondition condition);
	}
}