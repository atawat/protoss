using System.Linq;
using Protoss.Entity.Model;
using YooPoon.Core.Autofac;

namespace Protoss.Service.Member
{
	public interface IMemberService : IDependency
	{
		MemberEntity Create (MemberEntity entity);

		bool Delete(MemberEntity entity);

		MemberEntity Update (MemberEntity entity);

		MemberEntity GetMemberById (int id);

		IQueryable<MemberEntity> GetMembersByCondition(MemberSearchCondition condition);

		int GetMemberCount (MemberSearchCondition condition);
	}
}