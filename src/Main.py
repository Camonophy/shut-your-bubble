import sys

from PyQt5 import QtWidgets

class GUI(QtWidgets.QMainWindow):
    def __init__(self):
        super(GUI, self).__init__()
        
        # Main window
        self.setGeometry(200, 200, 800, 800)
        self.setWindowTitle("Shut Your Bubble")
        self.resize(900, 500)



if __name__ == "__main__":

    # Create the Qt Application
    app = QtWidgets.QApplication(sys.argv)

    # Create a Qt Window
    gui = GUI()
    
    #Run the Qt-GUI
    gui.show()


    sys.exit(app.exec_())
