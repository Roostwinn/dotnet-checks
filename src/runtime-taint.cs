using System;
using FastEndpoints;

public class MyEndpoint : Endpoint<MyRequest, MyResponse>
{
    public override void Configure()
    {
        Post("/api/user/create");
        AllowAnonymous();
    }

    public override async Task HandleAsync(MyRequest req, CancellationToken ct)
    {
        await SendAsync(new()
        {
            FullName = req.FirstName + " " + req.LastName,
            IsOver18 = req.Age > 18,
            // ruleid: runtime-taint
            Value = File.WriteAllLinesAsync(req.FirstName, "y")
        });
    }
}

public class MyEndpoint : Ep.Req<MyRequest>.Res<MyResponse> { 
    public override void Configure()
    {
        Post("/api/user/create");
        AllowAnonymous();
    }

    public override async Task HandleAsync(MyRequest req, CancellationToken ct)
    {
        await SendAsync(new()
        {
            FullName = req.FirstName + " " + req.LastName,
            IsOver18 = req.Age > 18,
            // ruleid: runtime-taint
            Value = File.WriteAllLinesAsync(req.FirstName, "y")
        });
    }
}