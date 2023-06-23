/*import logo from './logo.svg';*/
import { Routes, Route } from "react-router-dom";
import './App.css';
import AllRecipes from "./Components/AllRecipes";
import Main from './Components/Main';
import Menu from "./Components/Menu";
import Recipe from "./Components/Recipe";
import SelectedRecipes from "./Components/SelectedRecipes";
import Shoppinglist from "./Components/Shoppinglist";

function App() {
  return (
      <>
          <Routes>
              <Route index element={<Main />} />
              {/*<Route path="/" element={<Menu />} />*/}
              <Route path="/allrecipes" element={<AllRecipes />} />
              <Route path="/recipe:id" element={<Recipe />} />
              <Route path="/selectedrecipes" element={<SelectedRecipes />} />
              <Route path="/shoppinglist" element={<Shoppinglist/> }/>
          </Routes>
      </>
  );
}

export default App;
