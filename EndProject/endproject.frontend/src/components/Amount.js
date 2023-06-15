import { useState } from 'react';
function Amount() {
    const [Amount, setAmount] = useState('');

    const handleChange = (event) => {
        setAmount(event.target.value);
    };

    const handleSubmit = (event) => {
        alert("You would like " + Amount + " recipes this week");
        event.preventDefault();
    };
    
    return (
        <form onSubmit={handleSubmit}>
        <label>
        How many recipes do you want? 
            <select value={Amount} onChange={handleChange}>
            <option value="1">1</option>
            <option value="2">2</option>
            <option value="3">3</option>
            <option value="4">4</option>
            <option value="5">5</option>
            <option value="6">6</option>
            <option value="7">7</option>
                </select>
                <input type = "submit" value = "Submit"/>
            </label>
        </form>
    )
}

export default Amount