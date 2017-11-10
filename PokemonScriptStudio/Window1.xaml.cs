/*
 * Creado por SharpDevelop.
 * Usuario: Pikachu240
 * Fecha: 09/11/2017
 * Hora: 19:33
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Gabriel.Cat;
using PokemonGBAFrameWork;
using Gabriel.Cat.Extension;
using PokemonGBAFrameWork.Wpf;
namespace PokemonScriptStudio
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		VisualRom romSeleccionado;
		public Window1()
		{
			InitializeComponent();
		}
		void MiCargar_Click(object sender, RoutedEventArgs e)
		{
			throw new NotImplementedException();
		}
		void MiConfigurar_Click(object sender, RoutedEventArgs e)
		{
			configApp.Visibility = Visibility.Visible;
			tabMain.Visibility=Visibility.Hidden;
		}
		void MiSobre_Click(object sender, RoutedEventArgs e)
		{
			throw new NotImplementedException();
		}
		void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			
			const int ROMS = 1;
			const int SCRIPTS = 2;
			
			IReadOnlyList<FileInfo> files;
			VisualRom romCargado;
			switch (tabMain.SelectedIndex) {
				case ROMS:
					ugRoms.Children.Clear();
					if (System.IO.Directory.Exists(configApp.txtRutaRoms.Text)) {
						files =new System.IO.DirectoryInfo(configApp.txtRutaRoms.Text).GetAllFiles();
						for (int i = 0; i < files.Count; i++) {
							if(files[i].Extension.Contains(RomGba.EXTENSION))
							{
								try
								{
									romCargado = new VisualRom(files[i].FullName);
									romCargado.Seleccionado += RomSeleccionado;
									romCargado.DesSeleccionado += RomDesSeleccionado;
									ugRoms.Children.Add(romCargado);
								}
								catch { }//si hay un problema al cargar la rom.gba podria ser que no fuera de pokemon, asi que la descarto
							}
						}
					}
					break;
				case SCRIPTS:
					break;
			}
		}

		void RomSeleccionado(object sender, EventArgs e)
		{
			//lo que pasa cuando seleccionan una rom...podria servir para ayudar ha contextualizar el script actual
			romSeleccionado = sender as VisualRom;
			btnDecopilarScript.IsEnabled = true;
		}

		void RomDesSeleccionado(object sender, EventArgs e)
		{
			//Descontextualizar??
			romSeleccionado = null;
			btnDecopilarScript.IsEnabled = false;
		}
		void BtnDecopilarScript_Click(object sender, RoutedEventArgs e)
		{
			Script scriptACargar;

			try {
				scriptACargar = new Script(romSeleccionado.Rom, (int)(Hex)txtOffsetScript.Text);
				//de momento guardo el script
				AñadirScript(scriptACargar,romSeleccionado.Rom.Nombre+"."+txtOffsetScript.Text);
				
			} catch {
				MessageBox.Show("El offset no pertenece a ningun script bien formado!", "Atención", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}
		public void AñadirScript(Script script=null,string nombreScript="nuevoScript")
		{
			ScriptViwer svScript;
			TabItem tbScript;
			tbScript=new TabItem();
			tbScript.Header=nombreScript;
			svScript=new ScriptViwer(script);
			svScript.GuardarScript+=GuardarScript;
			tbScript.Content=svScript;
			tabScriptsAbiertos.Items.Add(tbScript);
		}

		void GuardarScript(object sender, EventArgs e)
		{
			ScriptViwer svScript=sender as ScriptViwer;
			string scriptPath;
			scriptPath = System.IO.Path.Combine(configApp.txtRutaScripts.Text, romSeleccionado.txtNombreRom.Text + "." + txtOffsetScript.Text + ".script");
			if (System.IO.File.Exists(scriptPath))
				System.IO.File.Delete(scriptPath);
			//mirar los scripts anidados...creo que van a dar complicaciones.
			System.IO.File.WriteAllBytes(scriptPath, svScript.Script.GetDeclaracion(romSeleccionado.Rom));
			
			MessageBox.Show("Script guardado satisfactoriamente!");
		}

		void ConfigApp_VolverAlMain(object sender, EventArgs e)
		{
			tabMain.Visibility=Visibility.Visible;
		}
	}
}