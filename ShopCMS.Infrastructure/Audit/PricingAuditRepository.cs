using ShopCMS.Domain.PricingAudit;
using ShopCMS.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ShopCMS.Infrastructure.Audit
{
    public class PricingAuditRepository : IPricingAuditRepository
    {
        private readonly ApplicationDbContext _context;

        public PricingAuditRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(PricingAudit audit, CancellationToken cancellationToken)
        {
            var entity = new PricingAuditEntity
            {
                Id = audit.Id,
                BasePrice = audit.BasePrice,
                FinalPrice = audit.FinalPrice,
                CreatedAt = audit.CreatedAt,
                RuleAudits = audit.RuleAudits.Select(r => new PricingRuleAuditEntity
                {
                    Id = Guid.NewGuid(),
                    RuleName = r.RuleName,
                    Applied = r.Applied,
                    InputPrice = r.InputPrice,
                    OutputPrice = r.OutputPrice,
                    Reason = r.Reason
                }).ToList()
            };

            _context.PricingAudits.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

}
