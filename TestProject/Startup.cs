using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace TestProject
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)

        {

            if (env.IsDevelopment())

            {

                app.UseDeveloperExceptionPage();

            }



            app.Use(async (context, next) =>

            {

                await next();

                if (context.Response.StatusCode == 404 && !System.IO.Path.HasExtension(context.Request.Path.Value))

                {

                    context.Request.Path = "/index.html";

                    await next();

                }

            });

            app.UseDefaultFiles();

            app.UseStaticFiles();



            app.UseCors(builder =>

            builder.AllowAnyOrigin()

            .AllowAnyHeader()

            .AllowAnyMethod()

            );



            app.UseAuthentication();



            app.UseMvc();



        }
    }
}
