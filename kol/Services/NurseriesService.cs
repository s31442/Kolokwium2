using kol.Exceptions;
using kol.DAL;
using kol.Models;
using kol.Models.DTOs;
using Microsoft.EntityFrameworkCore;



namespace kol.Services;

public class NurseriesService : INurseriesService
{
    private readonly BatchDbContext _DbContext;
    
    public NurseriesService(BatchDbContext dbContext)
    {
        _DbContext = dbContext;
    }

    public async Task<ReplyInformationsDTO> GetBatchesForIdAsync(int id, CancellationToken token)
    {
        if (id <= 0)
        {
            throw new BadRequestException("Id must be greater than 0!");
        }
        
        var nurs = await _DbContext.Nurseries.FindAsync(id, token);
        
        if (nurs == null)
            throw new NotFoundException("Nursery not found");

        var resp = new ReplyInformationsDTO
        {
            NurseryId = nurs.NurseryId,
            Name = nurs.Name,
            EstablishedDate = nurs.EstablishedDate,
        };

        var batches = await _DbContext.SeedlingBatches.Where(n => n.NurseryId == id).ToListAsync(token);

        resp.Batches = new List<ReplyBatchDTO>();
        
        foreach (var bat in batches)
        {
            var adding = new ReplyBatchDTO()
            {
                BatchId = bat.BatchId,
                Quantity = bat.Quantity,
                ReadyDate = bat.ReadyDate,
                SownDate = bat.SownDate,
                ReplySpecies = new ReplySpeciesDTO()
                {
                    LatinName = await _DbContext.TreeSpecies.Where(p=>bat.SpeciesId == p.SpeciesId).Select(p=>p.LatinName).FirstOrDefaultAsync(token),
                    
                    GrowthTimeInYears = await _DbContext.TreeSpecies.Where(p=>bat.SpeciesId == p.SpeciesId).Select(p=>p.GrowthTimeInYears).FirstOrDefaultAsync(token),
                },
                Responsible = new List<ReplyResponsibleDTO>()
                
            };
            
            var responsibeles = await _DbContext.Responsibles.Where(n => n.BatchId == bat.BatchId).ToListAsync(token);

            
            foreach (var res in responsibeles)
            {
                var responsible = new ReplyResponsibleDTO()
                {
                    Role = res.Role,
                    
                    FirstName = await _DbContext.Employees.Where(e => e.EmployeeId == res.EmployeeId).Select(e => e.FirstName).FirstOrDefaultAsync(token),
                    
                    LastName = await _DbContext.Employees.Where(e => e.EmployeeId == res.EmployeeId).Select(e => e.LastName).FirstOrDefaultAsync(token),
                };
                
                adding.Responsible.Add(responsible);

            }
            
            resp.Batches.Add(adding);

        }
        
        return resp;

    }

    public async Task InsertNewBatchAsync(RequestBatchDTO request, CancellationToken token)
    {
        var spec = await _DbContext.TreeSpecies.Where(p => p.LatinName == request.Species)
            .FirstOrDefaultAsync(token);
        if(spec == null)
            throw new NotFoundException("Species not found!");
        
        
        var nurs = await _DbContext.Nurseries.Where(p=>p.Name == request.Nursery).FirstOrDefaultAsync(token);
        
        if (nurs == null)
            throw new NotFoundException("Nursery not found!");
        
        using (var transaction = await _DbContext.Database.BeginTransactionAsync())
        {
            try
            {
                var batch = new Seedling_Batch()
                {
                    NurseryId = nurs.NurseryId,
                    SpeciesId = spec.SpeciesId,
                    Quantity = request.quantity,
                    SownDate = DateTime.Now
                };
                
                
                await _DbContext.SeedlingBatches.AddAsync(batch, token);
                
                await _DbContext.SaveChangesAsync(token);
                

                foreach (var req in request.Responsible)
                {
                    var employee = await _DbContext.Employees.Where(e => e.EmployeeId == req.EmployeeId).FirstOrDefaultAsync(token);
                    if (employee == null)
                        throw new NotFoundException("Employee not found!");

                    var responsible = new Responsible()
                    {
                        EmployeeId = req.EmployeeId,
                        BatchId = batch.BatchId,
                        Role = req.Role,
                    };
                    
                    await _DbContext.Responsibles.AddAsync(responsible);
                    await _DbContext.SaveChangesAsync(token);

                }
                
                await transaction.CommitAsync(token);
                
            }
            catch
            {
                await transaction.RollbackAsync();
                
                throw ;
            }
        }
    }
    
    
}