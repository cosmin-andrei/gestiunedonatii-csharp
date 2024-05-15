using GestiuneDonatii.model;
using System;
using System.Collections.Generic;

namespace GestiuneDonatii.Repository;

public interface IRepository <TId, TE> where TE: Entity<TId>
{
    public TE? findOne(TId id);
    IEnumerable<TE> findAll();
    public TE? save(TE Entity);
    public TE? delete(TId id);
    public TE? update(TE Entity);
}