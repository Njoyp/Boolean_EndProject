import Main from "./components/Main";
import Recipe from "./components/Recipe"
import { BrowserRouter, Routes, Route } from "react-router-dom";
import ReactDOM from "react-dom/client";

function App() {
  return (
      <div className="App">
          {/*<BrowserRouter>*/}
          {/*    <Routes>*/}
          {/*        <Route path="/" element={<Main /> }/>*/}
          {/*        <Route path="/recipe/:id" element={<Recipe/> }/>*/}
          {/*    </Routes>*/}
          {/*</BrowserRouter>*/}
          <Main />
          {/*<Recipe/>*/}
    </div>
  );
}
//const root = ReactDOM.createRoot(document.getElementById('root'));
//root.render(<App />);

export default App;
