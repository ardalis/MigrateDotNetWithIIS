# Migrate DotNet With IIS

Samples used in the [Porting Existing ASP.NET Apps to ASP.NET Core](https://aka.ms/aspnet-porting-ebook) ebook showing various IIS approaches to hosting ASP.NET and ASP.NET Core side by side.

## Scenario 1: .NET Core App inside .NET Framework App

This scenario allows you to migrate a portion of an existing ASP.NET app to ASP.NET Core by hosting one or more folders inside of the ASP.NET Site in IIS.

1. Publish the DotNetMvcApp to an IIS Site.
2. Publish AspNetCoreApi to a folder.
3. Open IIS Manager, navigate to the Site, right-click, Add Application. Name the application `api`. Point it at the folder specified in the previous step. Configure it with its own application pool set to No Managed Code.

You should be able to reach the AspNetCoreApi app by navigating to the main Site's `/api` path. Note that routes in the AspNetCoreApi project do not include `api` in them.

In this example, the AspNetCoreApi project is running on .NET 4.6.1 and should be able to use most of the same libraries as the original ASP.NET MVC app.

Note that you can repeat step 3 above for as many different folders as you want. If your original app had content under `Schools`, `Teachers`, and `Students` you could migrate these one by one to use the nested ASP.NET Core app by adding new applications and pointing them all to the same folder.

## Scenario 2: Gradually shift individual API endpoints/routes to ASP.NET Core / ASP.NET 5

Modify your hosts file so `api.contoso.com` and `api2.contoso.com` both map to IP address 127.0.0.1.

In this example, a separate site will be added. Name the original site `api.contoso.com` and create a new one `api2.contoso.com`. Publish the Net5Api project to `api2.contoso.com` and configure its IIS application pool touse No Managed Code.

You need to make sure Application Request Routing and URL Rewrite are installed on the IIS server.

At the IIS server level, configure Application Request Routing. Go to proxy settings. Check Enable proxy. Uncheck Reverse rewrite host in response headers.

Next, go to the .NET Framework app, `api.contoso.com`. Add a new rewrite rule. In the site's web.config the rule should look like this:

```
<system.webServer>
    <rewrite>
        <rules>
            <rule name="ReverseProxyInboundRule1" stopProcessing="true">
                <match url="api/values(.*)" />
                <action type="Rewrite" url="http://api2.contoso.com/{R:0}{R:1}" logRewrittenUrl="true" />
            </rule>
        </rules>
    </rewrite>
</system.webServer>
```

After restarting IIS you should be able to navigate to `api.contoso.com/api/states` and see it served by the ASP.NET MVC app. But when navigating to `api.contoso.com/api/values` you should see it served from the ASP.NET Core app running .NET 5.

## Publish Profiles

ASP.NET MVC 5 App (.NET Framework)
![image](https://user-images.githubusercontent.com/782127/107702937-f82d4300-6c88-11eb-8f3d-5fc8a2d43f68.png)

ASP.NET Core 2.1 App (running on .NET Framework net461)
![image](https://user-images.githubusercontent.com/782127/107703083-2ad73b80-6c89-11eb-9dbb-f216cc77a29e.png)

ASP.NET Core 5 App (running on .NET 5)
![image](https://user-images.githubusercontent.com/782127/107703181-50644500-6c89-11eb-939a-ca3042ffae88.png)
