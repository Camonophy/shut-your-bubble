from PyQt5 import QtWidgets


class GUI(QtWidgets.QMainWindow):
    def __init__(self):
        super(GUI, self).__init__()
        
        # Main window
        self.setGeometry(200, 200, 800, 800)
        self.setWindowTitle("Shut Your Bubble")
        self.resize(900, 500)
