import sys

from PyQt5         import QtWidgets
from GUI           import GUI
from Textinguisher import *

PATH = 'example/FEheroes.jpg'
X, Y, W, H = 550, 123, 446, 49

remove_section(PATH, X, Y, W, H)

# Create the Qt Application
#app = QtWidgets.QApplication(sys.argv)
#
## Create a Qt Window
#gui = GUI()
#    
##Run the Qt-GUI
#gui.show()
#
#sys.exit(app.exec_())
