# Sitecore PackMan
***TBD !!!!!! Here comes a little introduction.***

## Installation Instructions
You can either install the module with a Sitecore package over the `Installation Wizard` or compile the source code into your Sitecore installation.

### Package Installation
The module can be installed with the `Installation Wizard` in the Sitecore Desktop with the provided Sitecore package. For more information about package installations see [this blogpost](http://sitecoreguild.blogspot.ch/2013/03/quickstart-installing-sitecore-packages.html) (it's not written for Sitecore 8, but the process is the same). There are no additional post installation steps necessary, you can directly go and use the module.

### Compile Code
You can easily compile the complete code and deploy it to any Sitecore installation on your local environment. The following steps are necessary for a working deployment:

- Add a file `deploy.txt` into the `build` folder on the root directory of the source code. The file contains the path to your webroot, i.e. `D:\Sitecore8\Website`
- Add a new config file in the `App_Config\Include` folder with the following content (the path should point to the `serialization` folder of the source code:

		<configuration xmlns:patch="http://www.sitecore.net/xmlconfig/" xmlns:set="http://www.sitecore.net/xmlconfig/set/">
		  <sitecore>
		    <settings>
		      <setting name="SerializationFolder" set:value="D:\PackMan\serialization" />
		    </settings>
		  </sitecore>
		</configuration>

- Add `Sitecore.Kernel.dll` and `Sitecore.Client.dll` into the `lib` folder of the source code root. The assemblies must be copied from a Sitecore 8 installation.
- Open the solution and compile the code.
- Log into your Sitecore installation with an administrator.
- Go to `http://<your-sitecore-installation/unicorn.aspx` and click the button `Sync Default Configuration Now`. This should serialize all the needed items for the module.

You are now ready to use the module. If you make some changes to the code you can compile at any time. A post build event copies all needed files to your Sitecore installation. 

### Configuration
The module contains one config file called `Unic.PackMan.config` in the `App_Config\Include` folder. Mainly there are different pipeline where you could add additional processors if you need to. In the `<packman>` node it's also possible to configure include and exclude rules for the item tracking. Please read the instructions within the config file for further information.

### Compatibility
The module is compatible and tested with Sitecore 8 only.

## Usage
