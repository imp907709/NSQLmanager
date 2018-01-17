#stollen from 
#http://pbcraft.ru/simple-python3-web-server/
import http.server
import socketserver

from http.server import BaseHTTPRequestHandler, HTTPServer

PORT = 8000

class testHTTPServer_RequestHandler(BaseHTTPRequestHandler):
	def do_GET(self):
		# Отправляем код 200 (ОК)
		self.send_response(200)
		 # Теперь очередь за заголовками
		self.send_header('Content-type','text/html')
		self.end_headers()
	def do_POST(self):
		# Отправляем код 200 (ОК)
		self.send_response(200)
		 # Теперь очередь за заголовками
		self.send_header('Content-type','text/html')
		self.end_headers()
	def do_DELETE(self):
		# Отправляем код 200 (ОК)
		self.send_response(200)
		 # Теперь очередь за заголовками
		self.send_header('Content-type','text/html')
		self.end_headers()
		
Handler = http.server.SimpleHTTPRequestHandler

with socketserver.TCPServer(("", PORT), testHTTPServer_RequestHandler) as httpd:
    print("serving at port", PORT)
    httpd.serve_forever()