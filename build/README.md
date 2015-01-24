Before building this module in Visual Studio, be sure to make the following things:

- Add a `deploy.txt` in this folder and add the path to the Sitecore installation where the code should be copied to.
- Set the serialization path in `SerializationFolder` to the `serialization` folder in the root of the module. 

    <configuration xmlns:patch="http://www.sitecore.net/xmlconfig/" xmlns:set="http://www.sitecore.net/xmlconfig/set/">
      <sitecore>
        <settings>
          <setting name="SerializationFolder" set:value="D:\Sitecore\Modules\sitecore-module-packman\serialization" />
        </settings>
      </sitecore>
    </configuration>