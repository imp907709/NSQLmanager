from http.server import BaseHTTPRequestHandler, HTTPServer
import win32com.client
# HTTPRequestHandler class
class testHTTPServer_RequestHandler(BaseHTTPRequestHandler):
  
   def do_AUTHHEAD(self):
        print("send header")
        self.send_response(401)
        self.send_header('WWW-Authenticate', 'Basic realm=\"Test\"')
        self.send_header('Content-type', 'text/html')
        self.end_headers()
        
  # GET
  def do_GET(self):
        #auth
        global key
        ''' Present frontpage with user authentication. '''
        if self.headers.getheader('Authorization') == None:
            self.do_AUTHHEAD()
            self.wfile.write('no auth header received')
            pass
        elif self.headers.getheader('Authorization') == 'Basic '+key:
            SimpleHTTPRequestHandler.do_GET(self)
            pass
        else:
            self.do_AUTHHEAD()
            self.wfile.write(self.headers.getheader('Authorization'))
            self.wfile.write('not authenticated')
          pass
          
        #noauth
        '''
        # Send response status code
        self.send_response(200)
        self.do_AUTHHEAD()
        
        # Send headers
        self.send_header('Content-type','text/html')
        self.end_headers()

        # Send message back to client
        message = "Get Accepted!"
        # Write content as utf-8 data
        self.wfile.write(bytes(message, "utf8"))
        return
        '''
        
  # POST
  def do_POST(self):
        # Send response status code
        self.send_response(200)

        # Send headers
        self.send_header('Content-type','text/html')
        self.end_headers()
        
        content_length = int(self.headers['Content-Length']) # <--- Gets the size of data
        post_data = self.rfile.read(content_length) # <--- Gets the data itself
        message = "Post received!" + str(post_data)
        #self._set_headers()
        #self.wfile.write(post_data)
        self.wfile.write(bytes(message, "utf8"))
        
        '''
        # Send message back to client
        message = "Post received!"
        # Write content as utf-8 data
        self.wfile.write(bytes(message, "utf8"))
        '''
        
        return
		
  # PUT
  def do_PUT(self):
        # Send response status code
        self.send_response(200)

        # Send headers
        self.send_header('Content-type','text/html')
        self.end_headers()

        content_length = int(self.headers['Content-Length']) # <--- Gets the size of data
        post_data = self.rfile.read(content_length) # <--- Gets the data itself
        # Send message back to client
        message = "Put received!" + str(post_data)
        # Write content as utf-8 data
        self.wfile.write(bytes(message, "utf8"))

        return
		 # PUT
		 
  def do_DELETE(self):
        # Send response status code
        self.send_response(200)

        # Send headers
        self.send_header('Content-type','text/html')
        self.end_headers()
        
        content_length = int(self.headers['Content-Length']) # <--- Gets the size of data
        post_data = self.rfile.read(content_length) # <--- Gets the data itself
        
        # Send message back to client
        message = "Delete received!"+ str(post_data)
        # Write content as utf-8 data
        self.wfile.write(bytes(message, "utf8"))
        return
		
def run():
  print('starting server...')

  # Server settings
  # Choose port 8080, for port 80, which is normally used for a http server, you need root access
  server_address = ('127.0.0.1', 8000)
  httpd = HTTPServer(server_address, testHTTPServer_RequestHandler)
  print('running server...')
  httpd.serve_forever()


run()