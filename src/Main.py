import sys

from PyQt5         import QtWidgets
from GUI           import GUI
from Textinguisher import *

# Create the Qt Application
app = QtWidgets.QApplication(sys.argv)

# Create a Qt Window
gui = GUI()
    
#Run the Qt-GUI
gui.show()

sys.exit(app.exec_())
